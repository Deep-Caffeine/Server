using server.Interface;

namespace server.Services;

public class ChatHttpService : IChatHttpService
{
    private readonly ApplicationDbContext _context;

    public ChatHttpService(ApplicationDbContext context)
    {
        _context = context;
    }
}
