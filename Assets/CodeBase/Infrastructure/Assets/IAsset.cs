using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Assets
{
  public interface IAsset : IService
  {
    Task<GameObject> Instantiate(string address);
    Task<GameObject> Instantiate(string address, Transform parent);
    Task<GameObject> Instantiate(string address, Vector3 position, Quaternion quaternion);
    Task<T> Load<T>(string address) where T : class;
  }
}