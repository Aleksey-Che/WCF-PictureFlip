using System.ServiceModel;

namespace wcfPictureFlip
{
    [ServiceContract]
    public interface IServicePictureFlip
    {
        [OperationContract]
        void Execution();

        [OperationContract]
        object getPath(string dirName);

        [OperationContract]
        long doFlip(object listFiles, int angle, string dirName);
    }
}
