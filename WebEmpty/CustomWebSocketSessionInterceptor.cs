using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Subscriptions;
using HotChocolate.AspNetCore.Subscriptions.Protocols;
using WebEmpty.jwt;

namespace WebEmpty;

public class CustomWebSocketSessionInterceptor :  DefaultSocketSessionInterceptor
{
    private readonly IJwtHelper _jwtHelper;

    public CustomWebSocketSessionInterceptor(IJwtHelper jwtHelper)
    {
        _jwtHelper = jwtHelper;
    }


    public override ValueTask<ConnectionStatus> OnConnectAsync(ISocketSession session,
        IOperationMessagePayload connectionInitMessage,
        CancellationToken cancellationToken = new CancellationToken())
    {
        // 从 connectionInitMessage 获取令牌
        var dic = connectionInitMessage.As<Dictionary<string, string>>();
        if (dic.TryGetValue("authorization", out var tokenObj) &&
            tokenObj is { } token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new GraphQLException("Invalid authentication token.");
            }
            
            var tokenStr = token.Split(' ')[1];
            // 验证令牌，例如使用 JWT
            if (_jwtHelper.ValidateToken(tokenStr, out var claimsPrincipal))
            {
                // 将认证信息存储到上下文中
                if (claimsPrincipal != null) session.Connection.HttpContext.User = claimsPrincipal;
            }
            else
            {
                throw new GraphQLException("Invalid authentication token.");
            }
        }
        else
        {
           throw new GraphQLException("Authentication token is required.");
        }

        return base.OnConnectAsync(session, connectionInitMessage, cancellationToken);
    }
}