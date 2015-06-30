﻿using System;
using Microsoft.Practices.Unity;

namespace ENTech.Store.Infrastructure
{
	public class IoC
	{
		private static readonly Lazy<IUnityContainer> LazyContainer = new Lazy<IUnityContainer>(() => new UnityContainer());

		public static IUnityContainer Container
		{
			get { return LazyContainer.Value; }
		}

		public static T Resolve<T>()
		{
			return Container.Resolve<T>();
		}


		public static object Resolve(Type type)
		{
			return Container.Resolve(type);
		}
	}
}