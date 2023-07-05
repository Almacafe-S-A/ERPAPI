using System;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
	//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationsController : Controller
	{
		private readonly ApplicationDbContext context;
		private readonly ILogger logger;

		public NotificationsController(ApplicationDbContext context, ILogger<NotificationsController> logger)
		{
			this.context = context;
			this.logger = logger;
		}

		[HttpGet("[action]")]
		public async Task<ActionResult> GetAllNotifications()
		{
			try
			{
				var notifications = await context.Notifications.ToListAsync();
				return Ok(notifications);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error al cargar las Notificaciones");
				return BadRequest(ex);
			}
		}

		[HttpGet("[action]")]
		public async Task<ActionResult> GetNotifications()
		{
			try
			{
				var notifications = await context.Notifications.Where(e => !e.Leido).ToListAsync();
				return Ok(notifications);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error al cargar las Notificaciones");
				return BadRequest(ex);
			}
		}

		[HttpPost("[action]")]
		public async Task<ActionResult> PostNotification(Notifications notifications)
		{
			try
			{
				if (notifications.Id == 0)
				{
					Notifications f = context.Notifications
						.Where(q => q.Description == notifications.Description	&& ((notifications.FechaNotificacion <= q.FechaLectura) ||
						(q.FechaLectura >= notifications.FechaNotificacion))).FirstOrDefault();
					if (f != null)
					{
						return BadRequest("Ya existe una notificación con esta configuración");
					}
					context.Notifications.Add(notifications);

					//YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
					new appAuditor(context, logger, User.Identity.Name).SetAuditor();

					await context.SaveChangesAsync();
					return Ok(notifications);
				}
				else
				{
					Notifications notificationExistente = await context.Notifications.FirstOrDefaultAsync(f => f.Id == notifications.Id);
					if (notificationExistente == null)
					{
						throw new Exception("Registro de Notificación no existe");
					}

					context.Entry(notificationExistente).CurrentValues.SetValues(notifications);

					//YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
					new appAuditor(context, logger, User.Identity.Name).SetAuditor();

					await context.SaveChangesAsync();
					return Ok(notificationExistente);
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error al guardar Notificación");
				return BadRequest(ex);
			}
		}


		[HttpPost("[action]")]
		public async Task<ActionResult> DeleteNotification(Notifications notifications)
		{
			try
			{
				var existingNotification = context.Notifications.Where(q => q.Id == notifications.Id).FirstOrDefault();
				if (existingNotification != null)
				{
					context.Notifications.Remove(existingNotification);

					//YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
					new appAuditor(context, logger, User.Identity.Name).SetAuditor();

					await context.SaveChangesAsync();
					return Ok(notifications);
				}
				else
				{
					return BadRequest("No se encontro la Notificación");
				}


			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error al eliminar la Notificación");
				return BadRequest(ex);
			}
		}

		[HttpPost("[action]")]
		public async Task<ActionResult> MarkNotificationAsRead(Notifications notification)
		{
			try
			{
				var existingNotification = await context.Notifications.FindAsync(notification.Id);
				if (existingNotification != null)
				{
					existingNotification.Leido = true;
					existingNotification.FechaLectura = DateTime.Now;

					// YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
					new appAuditor(context, logger, User.Identity.Name).SetAuditor();

					await context.SaveChangesAsync();
					return Ok(existingNotification);
				}
				else
				{
					return BadRequest("No se encontró la notificación");
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error al marcar la notificación como leída");
				return BadRequest(ex);
			}
		}


	}
}
