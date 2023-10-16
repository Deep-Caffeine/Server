namespace server.Middlewares;

public class CorsMiddleware
{
    private readonly RequestDelegate mNext;

    public CorsMiddleware(RequestDelegate next)
    {
        mNext = next;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.Headers["Access-Control-Allow-Origin"] = "*";
        context.Response.Headers["Access-Control-Allow-Header"] = "*";
        context.Response.Headers["Access-Control-Allow-Method"] = "*";

        await mNext(context);
    }
}

