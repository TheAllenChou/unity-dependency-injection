using System;

namespace LongBunnyLabs.DependencyInjection
{
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
  public class InjectAttribute : Attribute
  {
    public string GameObjectName { get; } = "";

    public InjectAttribute(string gameObjectName = "")
    {
      GameObjectName = gameObjectName;
    }
  }
}

