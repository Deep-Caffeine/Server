using Microsoft.AspNetCore.SignalR;

namespace server.Controllers;

public class ChatHub :Hub
{
    public async Task SendMessgae(string user, string message)
    {
        Console.WriteLine($"SendMessage: {user}:{message}");
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
    public override async Task<Task> OnConnectedAsync()
    {
        Console.WriteLine($"[{DateTime.Now}] 연결 되었습니다.");
        await Groups.AddToGroupAsync(Context.ConnectionId, "Group Name");
        return base.OnConnectedAsync();
    }

    public override async Task<Task> OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"[{DateTime.Now}] 연결이 해제되었습니다.");
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Group Name");
        return base.OnDisconnectedAsync(exception);
    }
}
