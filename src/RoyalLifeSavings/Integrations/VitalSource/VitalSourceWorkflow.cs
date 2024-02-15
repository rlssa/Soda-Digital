using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Polly;
using Polly.Retry;
using RoyalLifeSavings.Data;
using static RoyalLifeSavings.Services.Policies;

namespace RoyalLifeSavings.Integrations.VitalSource
{
    public class VitalSourceWorkflow
    {
        private readonly VitalSourceService _vitalSource;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<VitalSourceWorkflow> _logger;

        public VitalSourceWorkflow(VitalSourceService vitalSource, UserManager<ApplicationUser> userManager, ILogger<VitalSourceWorkflow> logger)
        {
            _vitalSource = vitalSource;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task RunWorkflowAsync(string email, string? ebookId)
        {
            var user = await _userManager.FindByEmailAsync(email);

            try
            {
                if (string.IsNullOrEmpty(user.VitalSourceReferenceString))
                {
                    await Retry.ExecuteAsync(async () => await _vitalSource.CreateReferenceUserAsync(user));
                }
                var accessToken = await Retry.ExecuteAsync(async () => await _vitalSource.VerifyUserAsync(user));

                await _vitalSource.FullfilmentAsync(user, accessToken, ebookId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "VitalSource error");
                await _userManager.UpdateAsync(user);
            }

            await _userManager.UpdateAsync(user);
        }
    }
}
