using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Lanthanum.Data.Domain;
using Lanthanum.Data.Repositories;

namespace Lanthanum.Web.Services
{
    public class ActionRequestService
    {
        private readonly DbRepository<ActionRequest> _repository;

        public ActionRequestService(DbRepository<ActionRequest> repository)
        {
            _repository = repository;
        }

        public async Task<ActionRequest> GetThenRemoveActionRequestAsync(string code)
        {
            var actionRequest = await _repository
                .SingleOrDefaultAsync(x => x.RequestCode == code);
            
            if (actionRequest == null)
            {
                throw new ArgumentException("Code not found in database");
            }

            var copy = actionRequest.ShallowCopy();
            await _repository.RemoveAsync(actionRequest);
            return copy;
        }

        public async Task<ActionRequest> CreateActionRequestCodeAsync(User requestOwner)
        {
            var actionRequest = await _repository
                .SingleOrDefaultAsync(x => x.RequestOwner == requestOwner);
            if (actionRequest != null) await _repository.RemoveAsync(actionRequest);
            
            string bytesBase64Url;
            using( var generator = RandomNumberGenerator.Create() ) 
            {
                var bytes = new byte[12];
                generator.GetBytes( bytes );
                
                bytesBase64Url = Convert.ToBase64String( bytes ).Replace( '+', '-' ).Replace( '/', '_' );
            }

            actionRequest = new ActionRequest()
            {
                RequestCode = bytesBase64Url,
                RequestOwner = requestOwner
            };

            await _repository.AddAsync(actionRequest);
            return actionRequest;
        }
    }
}