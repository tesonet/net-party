using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using CommandLine;

namespace TestUtils
{
	public class AutoMoqDataAttribute : AutoDataAttribute
	{
		public AutoMoqDataAttribute() : base(() => new Fixture()
			.Customize(new AutoMoqCustomization() { ConfigureMembers = true })
			.Customize(new DataCustomization()))
		{
		}
	}

	public class DataCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize<Parser>(c => c.FromFactory(() => new Parser(settings => { settings.AutoHelp = false; })));
		}
	}
}