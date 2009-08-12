﻿using NUnit.Framework;
using SpecUnit;
using UoW.NHibernate;
using UoW.Specs.Model;

namespace UoW.Specs.NHibernate
{
	internal class MockFooRepo : NHibernateRepository, IFooRepository
	{
		public void Something()
		{
			Foo foo = new Foo();
			Session.Save(foo);
		}
	}

	[TestFixture]
	[Concern("NHibernateUoW")]
	public class When_shutting_down_NHibernateUoW : With_Valid_NHibernateConfig
	{

		private MockFooRepo foo;

		protected override void Context()
		{
			base.Context();

			foo = new MockFooRepo();

			UnitOfWork.Start(() =>
			{
				Transaction.Begin();
				NHibernateConfig.GenerateSchema();
				foo.Something();

				Assert.IsTrue(_uowStorage.HasTransactionManager);

				Transaction.Commit();
			});

		}

		[Test]
		[Observation]
		public void Should_clear_transaction_manager_from_storage()
		{
			Assert.IsFalse(_uowStorage.HasTransactionManager);
		}

	}
}
