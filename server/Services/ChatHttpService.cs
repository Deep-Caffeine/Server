using server.Interface;

namespace server.Services;

public class ChatHttpService : IChatHttpService
{
    private readonly ApplicationDbContext mContext;

    public ChatHttpService(ApplicationDbContext context)
    {
        mContext = context;
    }
}
