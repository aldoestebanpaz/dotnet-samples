using MyLibrary.Domain;

namespace MyLibrary.Api
{
    public class TodosApiService
    {
        static readonly Uri BASE_URL = new Uri("https://jsonplaceholder.typicode.com");
        private HttpClient _httpClient;

        public TodosApiService(HttpClient httpClient)
        {
            // HttpClient is intended to be instantiated once and reused throughout the life of an application.
            _httpClient = httpClient;
        }

        public async Task<Todo> FetchTodo(int id)
        {
            Uri uri = new Uri(BASE_URL, $"/todos/{id}");

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);

                // When you send a request and get an error status code (anything
                // outside of the 200 - 299 range), the EnsureSuccessStatusCode()
                // method throws an exception. It throws an HttpRequestException,
                // which is the same thing HttpClient throws when the request fails
                // for other reasons (such as connection failures). This simplifies
                // error handling, because you only need to catch one type of
                // exception.
                // Note: HttpClient throws a different type of exception for
                // timeouts (TaskCanceledException).
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Todo>();
            }
            catch (HttpRequestException ex)
            {
                // log or checks here

                // You should always use the following syntax to rethrow an exception.
                // Else you'll stomp the stack trace. If you print the trace resulting
                // from `throw ex`, you'll see that it ends on that statement and not
                // at the real source of the exception.
                throw;
            }
            catch (TaskCanceledException ex)
            {
                // log or checks here

                throw;
            }
        }

        public async Task<IEnumerable<Todo>> FetchTodos()
        {
            Uri uri = new Uri(BASE_URL, "/todos");

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<IEnumerable<Todo>>();
            }
            catch (HttpRequestException ex)
            {
                throw;
            }
            catch (TaskCanceledException ex)
            {
                throw;
            }
        }
    }
}
