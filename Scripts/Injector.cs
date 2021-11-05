using System.Reflection;

using UnityEngine;

namespace LongBunnyLabs.DependencyInjection
{
  public sealed class Injector
  {
    public static void Inject(Component component)
    {
      var componentType = component.GetType();
      var gameObject = component.gameObject;

      var fields = componentType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
      for (int i = 0; i < fields.Length; ++i)
      {
        var field = fields[i];

        var injects = (InjectAttribute[]) field.GetCustomAttributes(typeof(InjectAttribute), true);
        if (injects.Length <= 0)
          continue;

        bool isComponent = field.FieldType.IsSubclassOf(typeof(Component));
        bool isGameObject = field.FieldType.IsEquivalentTo(typeof(GameObject));

        if (!isComponent && !isGameObject)
        {
          Debug.LogWarning($"Dependency Injection: Field '{field.Name}' of component '{componentType.FullName}' is not of valid type 'UnityEngine.Component' nor 'UnityEngine.GameObject'.");
          continue;
        }

        var inject = injects[0];
        GameObject go = 
          string.IsNullOrEmpty(inject.GameObjectName) 
            ? gameObject 
            : GameObject.Find(inject.GameObjectName);

        if (go == null)
        {
          Debug.LogWarning($"Dependency Injection: Can't find game object '{inject.GameObjectName}' when injecting field '{field.Name}' of component '{componentType.FullName}'.");
          continue;
        }

        Object value = go;
        if (isComponent)
        {
          value = go.GetComponent(field.FieldType);
          if (value == null)
          {
            Debug.LogWarning($"Dependency Injection: Can't find component '{field.FieldType.FullName}' in game object '{go.name}' to inject into field '{field.Name}' of component '{componentType.FullName}' of game object '{gameObject.name}'.");
            continue;
          }
        }

        field.SetValue(component, value);
      }
    }
  }
}

