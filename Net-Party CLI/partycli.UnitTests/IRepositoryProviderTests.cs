using System.Collections.Generic;
using NUnit.Framework;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace partycli.UnitTests
{
    public class IRepositoryProviderTests
    {

        [Test]
        public void FileReposiotryProvider_ValidateReset_ShouldRemoveFileIfExists()
        {
            //Arrange            
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\Foo.txt", new MockFileData("Bar") }
            });
            var component = new FileRepositoryProvider(@"c:\Foo.txt", fileSystem as IFileSystem);

            //Act
            component.Reset();

            //Assert
            Assert.False(fileSystem.FileExists(@"c:\Foo.txt"));            
        }

        [Test]
        public void FileReposiotryProvider_ValidateSave_ShouldCreatFileAsync()
        {
            //Arrange            
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>{});
            var component = new FileRepositoryProvider(@"c:\Foo.txt", fileSystem as IFileSystem);

            //Act
            component.SaveAsync("Bar").Wait();

            //Assert
            Assert.True(fileSystem.FileExists(@"c:\Foo.txt"));
        }

        [Test]
        public void FileReposiotryProvider_ValidateSave_ShouldWriteToFile()
        {
            //Arrange
            var mockFileSystem = new MockFileSystem();
            var sut = new FileRepositoryProvider(@"C:\Foo.txt", mockFileSystem);

            //Act
            sut.SaveAsync("Bar").Wait();

            //Assert
            Assert.AreEqual("Bar", mockFileSystem.GetFile(@"C:\Foo.txt").TextContents) ;
        }


        [Test]
        public void FileReposiotryProvider_ValidateLoad_ShouldRetrieveTextFromFile()
        {
            // Arrange            
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\Foo.txt", new MockFileData("Bar1") }
            });
            var component = new FileRepositoryProvider(@"c:\Foo.txt", fileSystem as IFileSystem);

            // Act
            string result = component.LoadAsync().Result;

            //Assert
            Assert.AreEqual("Bar1", result);
        }
    }
}
