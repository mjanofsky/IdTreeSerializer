using NUnit.Framework;
using WBSTree;

namespace IdTreeSerializerTests
{
    [TestFixture]
    public class IdTreeTests
    {
        [Test]
        public void CanCreateEmptyIdTree()
        {
            var idTree = new IdTree();

            Assert.That(idTree.RootNode.Id, Is.EqualTo(IdNode.EmptyNodeId));
        }
        [Test]
        public void CanCreateIdTreeWithSingleNode()
        {
            const int rootNodeId = 1;
            var idNode = new IdNode(rootNodeId);
            var idTree = new IdTree(idNode);

            Assert.IsNotNull(idTree);
            Assert.That(idTree.RootNode.Id, Is.EqualTo(rootNodeId));
        }
    }
}