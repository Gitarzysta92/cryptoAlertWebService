using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BinaryObjectStorage.Interfaces;
using BinaryObjectStorage.Models;
using Microsoft.Extensions.Configuration;

namespace BinaryObjectStorage;

public class BinaryObjectStorageContext
{
	public StorageContainer<CoinIconFile> CoinIcons;
	public StorageContainer<ExchangeIconFile> ExchangeIcons { get; set; }
	
	public BinaryObjectStorageContext(IConfiguration configuration)
	{
		var connectionString = configuration["BlobConnection"] ?? configuration.GetConnectionString("BlobConnection");
		var blobServiceClient = new BlobServiceClient(connectionString);
		CoinIcons = new StorageContainer<CoinIconFile>(blobServiceClient, "coin-icons");
		ExchangeIcons = new StorageContainer<ExchangeIconFile>(blobServiceClient, "exchange-icons");
	}
}

public class StorageContainer<T> where T : StorageContainerItem
{
	private readonly string _containerName;
	private BlobServiceClient _blobServiceClient { get; set; }
	private BlobContainerClient? _blobContainerClient { get; set; }

	public StorageContainer(BlobServiceClient blobServiceClient, string containerName)
	{
		_blobServiceClient = blobServiceClient;
		_containerName = containerName;
	}
	
	
	public async Task Upload(string localFilePath, T file)
	{
		var containerClient = CreateBlobServiceClient();
		BlobClient blobClient = containerClient.GetBlobClient(file.FileName);
		await blobClient.UploadAsync(localFilePath, true);
	}

	public bool CheckIsAnyBlobExists()
	{	
		var containerClient = CreateBlobServiceClient();
		var blobs = containerClient.GetBlobs();
		return blobs.Any();
	}

	private BlobContainerClient CreateBlobServiceClient()
	{
		if (_blobContainerClient == null)
			return _blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
		
		return _blobContainerClient;
	}
}

