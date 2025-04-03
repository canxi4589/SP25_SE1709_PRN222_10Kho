using Azure.Storage.Blobs;

namespace CCPProject.Extension
{
    public static class BlobStorageExtension
    {
        public static IServiceCollection AddBlobService(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            var blobConnectionString = config["AzureStorage:BlobConnectionString"];
            services.AddScoped(_ => new BlobServiceClient(blobConnectionString));
            return services;
        }
    }
}
