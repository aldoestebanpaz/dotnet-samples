using MyLibrary.Api;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace MyLibrary.Tests.Api
{
    public class TodosApiServiceTests : IDisposable
    {
        private MockRepository _mockRepository;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;

        public TodosApiServiceTests()
        {
            // setup code
            _mockRepository = new(MockBehavior.Default);
            _mockHttpMessageHandler = _mockRepository.Create<HttpMessageHandler>();
        }

        public void Dispose()
        {
            // clean-up code
        }

        [Fact]
        public async Task FetchTodos_WhenInvoked_ShouldReturnAListOfTodos()
        {
            // arrange
            string responsePayload = JsonConvert.SerializeObject(new []
            {
                new { userId = 1, id = 1, title = "delectus aut autem", completed = false },
                new { userId = 1, id = 2, title = "quis ut nam facilis et officia qui", completed = false },
                new { userId = 1, id = 3, title = "fugiat veniam minus", completed = true },
            });
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responsePayload, Encoding.UTF8, "application/json")
                })
                .Verifiable();
            TodosApiService todosApiService = new(new HttpClient(_mockHttpMessageHandler.Object));

            // act
            var todos = await todosApiService.FetchTodos();

            // assert
            Expression<Func<HttpRequestMessage, bool>> httpRequestMessageVerifier = x =>
                x.RequestUri!.AbsoluteUri.Equals("https://jsonplaceholder.typicode.com/todos");
            _mockHttpMessageHandler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Once(),
                    ItExpr.Is(httpRequestMessageVerifier),
                    ItExpr.IsAny<CancellationToken>()
                );
            Assert.NotNull(todos);
            var todosArray = todos.ToArray();
            Assert.Equal(3, todosArray.Length);
            Assert.Equal(2, todosArray[1].Id);
            Assert.Equal(1, todosArray[1].UserId);
            Assert.Equal("quis ut nam facilis et officia qui", todosArray[1].Title);
            Assert.False(todosArray[1].Completed);
        }

        [Fact]
        public async Task FetchTodo_WhenInvoked_ShouldReturnASingleTodo()
        {
            // arrange
            string responsePayload = JsonConvert.SerializeObject(
                new { userId = 1, id = 17, title = "delectus aut autem", completed = false }
            );
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responsePayload, Encoding.UTF8, "application/json")
                })
                .Verifiable();
            TodosApiService todosApiService = new(new HttpClient(_mockHttpMessageHandler.Object));

            // act
            var todo = await todosApiService.FetchTodo(17);

            // assert
            Expression<Func<HttpRequestMessage, bool>> httpRequestMessageVerifier = x =>
                x.RequestUri!.AbsoluteUri.Equals("https://jsonplaceholder.typicode.com/todos/17");
            _mockHttpMessageHandler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Once(),
                    ItExpr.Is(httpRequestMessageVerifier),
                    ItExpr.IsAny<CancellationToken>()
                );
            Assert.NotNull(todo);
            Assert.Equal(17, todo.Id);
            Assert.Equal(1, todo.UserId);
            Assert.Equal("delectus aut autem", todo.Title);
            Assert.False(todo.Completed);
        }
    }
}
