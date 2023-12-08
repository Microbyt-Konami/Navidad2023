using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace microbytkonamic.proxy
{
    /*
        {
			"type": "https://tools.ietf.org/html/rfc9110#section-15.6.1",
			"title": "System.InvalidOperationException",
			"status": 500,
			"detail": "An exception has been raised that is likely due to a transient failure. Consider enabling transient error resiliency by adding 'EnableRetryOnFailure()' to the 'UseMySql' call.",
			"traceId": "00-296919d68c5f294df0b9db528e1146dc-0d877aed0071a2b4-00",
			"exception": {
				"details": "System.InvalidOperationException: An exception has been raised that is likely due to a transient failure. Consider enabling transient error resiliency by adding 'EnableRetryOnFailure()' to the 'UseMySql' call.\r\n ---> MySqlConnector.MySqlException (0x80004005): Unable to connect to any of the specified MySQL hosts.\r\n   at MySqlConnector.Core.ServerSession.OpenTcpSocketAsync(ConnectionSettings cs, ILoadBalancer loadBalancer, Activity activity, IOBehavior ioBehavior, CancellationToken cancellationToken) in /_/src/MySqlConnector/Core/ServerSession.cs:line 1105\r\n   at MySqlConnector.Core.ServerSession.ConnectAsync(ConnectionSettings cs, MySqlConnection connection, Int64 startingTimestamp, ILoadBalancer loadBalancer, Activity activity, IOBehavior ioBehavior, CancellationToken cancellationToken) in /_/src/MySqlConnector/Core/ServerSession.cs:line 444\r\n   at MySqlConnector.Core.ConnectionPool.ConnectSessionAsync(MySqlConnection connection, Action`4 logMessage, Int64 startingTimestamp, Activity activity, IOBehavior ioBehavior, CancellationToken cancellationToken) in /_/src/MySqlConnector/Core/ConnectionPool.cs:line 428\r\n   at MySqlConnector.Core.ConnectionPool.ConnectSessionAsync(MySqlConnection connection, Action`4 logMessage, Int64 startingTimestamp, Activity activity, IOBehavior ioBehavior, CancellationToken cancellationToken) in /_/src/MySqlConnector/Core/ConnectionPool.cs:line 433\r\n   at MySqlConnector.Core.ConnectionPool.GetSessionAsync(MySqlConnection connection, Int64 startingTimestamp, Int32 timeoutMilliseconds, Activity activity, IOBehavior ioBehavior, CancellationToken cancellationToken) in /_/src/MySqlConnector/Core/ConnectionPool.cs:line 113\r\n   at MySqlConnector.Core.ConnectionPool.GetSessionAsync(MySqlConnection connection, Int64 startingTimestamp, Int32 timeoutMilliseconds, Activity activity, IOBehavior ioBehavior, CancellationToken cancellationToken) in /_/src/MySqlConnector/Core/ConnectionPool.cs:line 146\r\n   at MySqlConnector.MySqlConnection.CreateSessionAsync(ConnectionPool pool, Int64 startingTimestamp, Activity activity, Nullable`1 ioBehavior, CancellationToken cancellationToken) in /_/src/MySqlConnector/MySqlConnection.cs:line 919\r\n   at MySqlConnector.MySqlConnection.OpenAsync(Nullable`1 ioBehavior, CancellationToken cancellationToken) in /_/src/MySqlConnector/MySqlConnection.cs:line 419\r\n   at Microsoft.EntityFrameworkCore.Storage.RelationalConnection.OpenInternalAsync(Boolean errorsExpected, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.Storage.RelationalConnection.OpenInternalAsync(Boolean errorsExpected, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.Storage.RelationalConnection.OpenAsync(CancellationToken cancellationToken, Boolean errorsExpected)\r\n   at Pomelo.EntityFrameworkCore.MySql.Storage.Internal.MySqlRelationalConnection.OpenAsync(CancellationToken cancellationToken, Boolean errorsExpected)\r\n   at Microsoft.EntityFrameworkCore.Storage.RelationalConnection.BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.Storage.RelationalConnection.BeginTransactionAsync(CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChangesAsync(IList`1 entriesToSave, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChangesAsync(StateManager stateManager, Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken)\r\n   at Pomelo.EntityFrameworkCore.MySql.Storage.Internal.MySqlExecutionStrategy.ExecuteAsync[TState,TResult](TState state, Func`4 operation, Func`4 verifySucceeded, CancellationToken cancellationToken)\r\n   --- End of inner exception stack trace ---\r\n   at Pomelo.EntityFrameworkCore.MySql.Storage.Internal.MySqlExecutionStrategy.ExecuteAsync[TState,TResult](TState state, Func`4 operation, Func`4 verifySucceeded, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken)\r\n   at MicroBytKonamic.Application.Services.PostalesServices.AltaFelicitacion(AltaFelicitacionIn input, CancellationToken cancellationToken) in E:\\Proyectos\\microbytkonamic\\WebSite\\MicroBytKonamic.Application\\Services\\PostalesServices.cs:line 59\r\n   at lambda_method4(Closure, Object)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeInnerFilterAsync>g__Awaited|13_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextResourceFilter>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ResourceExecutedContextSealed context)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)\r\n   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)",
				"headers": {
					"Accept": [
						"*//*"
					],
					"Host": [
						"localhost:7076"
					],
					"User-Agent": [
						"UnityPlayer/2022.3.13f1 (UnityWebRequest/1.0, libcurl/8.4.0-DEV)"
					],
					"Accept-Encoding": [
						"deflate, gzip"
					],
					"Content-Type": [
						"application/json"
					],
					"Content-Length": [
						"104"
					],
					"X-Unity-Version": [
						"2022.3.13f1"
					]
		},
				"path": "/api/postales/altafelicitacion",
				"endpoint": "MicroBytKonamic.Web.Controllers.api.PostalesController.AltaFelicitacion (MicroBytKonamic.Web)",
				"routeValues": {
			"action": "AltaFelicitacion",
					"controller": "Postales"

				}
			}
		}
     */

    [Serializable]
    public class WebApiProblemDetailsHeaders
    {
        public string[] Accept;
        public string[] Host;
        public string[] User_Agent;
        public string[] Accept_Encoding;
        public string[] Content_Type;
        public string[] Content_Length;
        public string[] X_Unity_Version;

        public override string ToString() => JsonUtility.ToJson(this);
    }

    [Serializable]
    public class WebApiProblemDetails_Exception
    {
        public string details;
        public WebApiProblemDetailsHeaders headers;
        public string path;
        public string endpoint;
        public string[] routeValues;

        public override string ToString() => JsonUtility.ToJson(this);
    }

    [Serializable]
    public class WebApiProblemDetails
    {
        public string title;
        public int status;
        public string detail;
        public string traceId;
        public WebApiProblemDetails_Exception exception;

        public override string ToString() => JsonUtility.ToJson(this);

        public static bool TryParseFromJson(string json, out WebApiProblemDetails problemDetails)
        {
            try
            {
                problemDetails = JsonUtility.FromJson<WebApiProblemDetails>(json);

                return true;
            }
            catch
            {
                problemDetails = null;

                return false;
            }
        }
    }
}
