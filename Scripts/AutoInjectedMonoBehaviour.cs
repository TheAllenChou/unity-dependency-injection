using UnityEngine;

namespace LongBunnyLabs.DependencyInjection
{
  public class AutoInjectedMonoBehaviour : MonoBehaviour
  {
    virtual protected void Start()
    {
      Injector.Inject(this);
    }
  }
}

