using System.ServiceModel;
using SoapService.DataContract;

namespace SoapService.ServiceContract;

[ServiceContract]
public interface IUserService
{
    [OperationContract]
    public string RegisterUser(User user);
}