## A Simple Dependency Injection Utility for Unity
### (Oops. It's actually a service locator rathern than dependency injection. Will clean up the terms later...)
by **Ming-Lun "Allen" Chou** / [AllenChou.net](http://AllenChou.net) / [@TheAllenChou](http://twitter.com/TheAllenChou) / [Patreon](https://www.patreon.com/TheAllenChou)

[Installation Guide](https://docs.unity3d.com/Manual/upm-ui-giturl.html)

## Usage

Add the `Inject` attribute to fields of type `GameObject` or `Component`. An additional game object name can be provided to the `Inject` attribute to specify which game object to inject from. If no game object name is provided, the game object of the component will be used.

Call `Injector.Inject` to inject into fields tagged with the `Inject` attribute.

```cs
using UnityEngine;

using LongBunnyLabs.DependencyInjection;

public class MyComponent : MonoBehaviour
{
  [Inject] private GameObject MyGameObject;
  [Inject] private Transform MyTransform;
  [Inject] private MeshRenderer MyRenderer;
  [Inject("Other")] private GameObject OtherGameObject;
  [Inject("Other")] private Transform OtherTransform;
  [Inject("Other")] private MeshRenderer OtherRenderer;

  private void Start()
  {
    Injector.Inject(this);
  }
}
```

This is equivalent to:

```cs
using UnityEngine;

public class MyComponent : MonoBehaviour
{
  private GameObject MyGameObject;
  private Transform MyTransform;
  private MeshRenderer MyRenderer;
  private GameObject OtherGameObject;
  private Transform OtherTransform;
  private MeshRenderer OtherRenderer;

  private void Start()
  {
    MyGameObject    = gameObject;
    MyTransform     = gameObject.GetComponent<Transform>();
    MyRenderer      = gameObject.GetComponent<MeshRenderer>();
    OtherGameObject = GameObject.Find("Other");
    OtherTransform  = GameObject.Find("Other").GetComponent<Transform>();
    OtherRenderer   = GameObject.Find("Other").GetComponent<MeshRenderer>();
  }
}
```

You can also inherit your component from `AutoInjectedMonoBehaviour`, whose `Start` method calls `Injector.Inject` so you don't have to.

```cs
using UnityEngine;

using LongBunnyLabs.DependencyInjection;

public class MyComponent : AutoInjectedMonoBehaviour
{
  [Inject] private GameObject Self;
  [Inject] private Transform MyTransform;
  [Inject] private MeshRenderer MyRenderer;
  [Inject("Other")] private GameObject Other;
  [Inject("Other")] private Transform TheirTransform;
  [Inject("Other")] private MeshRenderer TheirRenderer;
}
```

If you need to override the `Start` method, however, you'd need to remember to call `base.Start`.

```cs
using UnityEngine;

using LongBunnyLabs.DependencyInjection;

public class MyComponent : AutoInjectedMonoBehaviour
{
  [Inject] private GameObject Self;
  [Inject] private Transform MyTransform;
  [Inject] private MeshRenderer MyRenderer;
  [Inject("Other")] private GameObject Other;
  [Inject("Other")] private Transform TheirTransform;
  [Inject("Other")] private MeshRenderer TheirRenderer;
  
  protected override void Start()
  {
    base.Start();
    
    // your code here
  }
}
```

`Injector.Inject` doesn't have to be called within a `Start` method. You can call it whereever you see fit.
