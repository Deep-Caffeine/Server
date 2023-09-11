using Microsoft.AspNetCore.Mvc;
using server.Services;

namespace server.Controllers;

[Route("chat")]
[ApiController]
public class ChatHttpController : ControllerBase
{
    public readonly ChatHttpService mChatHttpService;

    public ChatHttpController(ChatHttpService chatHttpService)
    {
        mChatHttpService = chatHttpService;
    }
}
