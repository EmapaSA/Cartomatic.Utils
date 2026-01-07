using System;
using AwesomeAssertions;
using NUnit.Framework;

namespace Cartomatic.Utils.PathExtensions.Tests
{
    [TestFixture]
    public class PathExtensionsTests
    {
        [Test]
        public void AbsolutePath_WhenCheckedIfAbsolute_ShouldReturnTrue()
        {
            var absPath = @"c:\test\path.txt";

            absPath.IsAbsolute().Should().BeTrue();
        }

        [Test]
        public void RootedPath_WhenCheckedIfAbsolute_ShouldReturnFalse()
        {
            var absPath = @"\test\path.txt";

            absPath.IsAbsolute().Should().BeFalse();
        }

        [Test]
        public void RelativePath_WhenCheckedIfAbsolute_ShouldReturnFalse()
        {
            var absPath = @"test\path.txt";

            absPath.IsAbsolute().Should().BeFalse();
        }

        [TestCase(@"c:\tem|<p\fi<>le.txt")]
        [TestCase(@"|<>!#$%test\path.txt")] //because of some reason <> does not throw... dunno why. it should
        public void InvalidPath_WhenCheckedIfAbsolute_ShouldThrow(string invalidPath)
        {
            Action act = () => { invalidPath.IsAbsolute(); };
            act.Should().Throw<Exception>();
        }

        [Test]
        public void ValidAbsolutePath_WhenSolved_ShouldBeSame()
        {
            var absPath = @"c:\test\path.txt";

            absPath.SolvePath().Should().Be(absPath);
        }

        [Test]
        public void RelativePath_WhenSolved_ShouldBeRelativeToTheExecutingAssembly()
        {
            var relativePath = @"test\path.txt";
            //get the drive of executing assembly
            var pathToExecutingAssembly = AppDomain.CurrentDomain.BaseDirectory;
            var whatShouldBeAfterSolving = System.IO.Path.Combine(pathToExecutingAssembly, relativePath);

            relativePath.SolvePath().Should().Be(whatShouldBeAfterSolving);
        }

        [Test]
        public void RootedPath_WhenSolved_ShouldBeRelatedToRootOfTheExecutingAssembly()
        {
            var rootedPath = @"\test\path.txt";
            //get the drive of executing assembly
            var rootOfExecutingAssembly = System.IO.Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory);
            var whatShouldBeAfterSolving = System.IO.Path.Combine(rootOfExecutingAssembly, rootedPath);

            rootedPath.SolvePath().Should().Be(whatShouldBeAfterSolving);
        }

        [TestCase(@"c:\tem|<p\fi<>le.txt")]
        [TestCase(@"|<>!#$%test\path.txt")] //because of some reason <> does not throw... dunno why. it should
        public void InvalidPath_WhenSolved_ShouldBeNull(string invalidPath)
        {
            invalidPath.SolvePath().Should().Be(null);
        }


        [Test(Description = "Windows only test!")]
        public void IsDirectory_WhenExists_ShouldBeTrue()
        {
            var path = @"c:\windows";

            path.IsDirectory().Should().BeTrue();
        }

        [Test(Description = "Windows only test!")]
        public void IsDirectory_WhenIsRealFile_ShouldBeFalse()
        {
            var path = @"c:\windows\regedit.exe";

            path.IsDirectory().Should().BeFalse();
        }

        [Test]
        public void IsDirectory_WhenNotExistsButLooksLikeDir_ShouldBeTrue()
        {
            var path = System.IO.Path.Combine(@"c:\", Guid.NewGuid().ToString());

            path.IsDirectory().Should().BeTrue();
        }

        [Test]
        public void IsDirectory_WhenNotExistsAndLooksLikeFile_ShouldBeFalse()
        {
            var path = $@"c:\{Guid.NewGuid().ToString()}.txt";

            path.IsDirectory().Should().BeFalse();
        }
    }
}