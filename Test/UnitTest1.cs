using Ini;
using Xunit;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var path = "People.ini";

            var savePeople = new People()
            {
                Id = 3239198,
                Age = 42,
                IsMarried = false,
                Name = "Arin",
                Sex = Sex.NON_SEX,
                Sex2 = Sex.FEMALE
            };

            IniMapper.Save(savePeople, path);

            var loadPeople = IniMapper.Load<People>(path);

            Assert.Equal(savePeople, loadPeople);
        }

        [Fact]
        public void Test2()
        {
            var path = "TEST.ini";

            var save = new TestModel()
            {
                Index = 0x25,
                IsMaximalSized = true,
                Offset = 0xff
            };

            IniMapper.Save(save, path);

            var load = IniMapper.Load<TestModel>(path);

            Assert.Equal(save, load);
            Assert.NotEqual(save.Offset, load.Offset);
            Assert.Equal(save.Offset, (byte)0xff);
            Assert.Equal(load.Offset, (byte)0);
        }
    }
}