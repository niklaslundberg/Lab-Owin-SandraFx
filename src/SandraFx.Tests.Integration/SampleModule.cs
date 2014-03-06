namespace SandraFx.Tests.Integration
{
    public class SampleModule : NancyModule
    {
        public SampleModule()
        {
            Get["/"] = _ => "Hello world";
        }
    }
}