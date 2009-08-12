﻿using NUnit.Framework;
using SpecUnit;

namespace UoW.Specs
{

	public abstract class RepositoryExecutionContext : BaseMockUoWContext {}

	[TestFixture]
	[Concern("Transaction Execution")]
	public class When_executing_a_repository_method : RepositoryExecutionContext
	{

		private class MockFooRepo : IFooRepo
		{
			#region IFooRepo Members

			public void Something()
			{
			}

			#endregion
		}

		private interface IFooRepo
		{
			void Something();
		}

		private IFooRepo fooRepo;

		protected override void Context()
		{
			fooRepo = new MockFooRepo();
			UnitOfWork.Start(() => 
				fooRepo.Something()
			);
		}

		[Test]
		[Observation]
		public void Should_retrieve_an_instance_of_the_specified_repository()
		{
			fooRepo.ShouldNotBeNull();
		}

	}

}