using UnityEngine;

namespace LongBunnyLabs.DependencyInjection
{
  public class InjectedMonoBehaviour : MonoBehaviour
  {
    virtual protected void Start()
    {
      Injector.Inject(this);
    }
  }
}

