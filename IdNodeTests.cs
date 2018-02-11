using WBSTree;
using NUnit.Framework;

namespace IdTreeSerializerTests
{
    [TestFixture]
    public class IdNodeTests
    {
        [Test]
        public void CanCreateAnEmptyIdNode() 
        {
            var idNode = new IdNode();
            Assert.IsNotNull(idNode);
        }

        [Test]
        public void CanCreateIdNodeWithNoChildrenNoParent()
        {
            const int nodeId = 1;
            var idNode = new IdNode(nodeId);

            Assert.That(idNode.Id, Is.EqualTo(nodeId));
            Assert.That(idNode.Parent.Id, Is.EqualTo(IdNode.EmptyNodeId));
            Assert.That(idNode.Children.Count, Is.EqualTo(0));
        }

        [Test]
        public void CanCreateIdNodeWithParent()
        {
            const int nodeId = 2;
            const int parentNodeId = 1;

            var idNode = new IdNode(nodeId, new IdNode(parentNodeId));

            Assert.IsNotNull(idNode);
            Assert.That(idNode.Id, Is.EqualTo(nodeId));
            Assert.That(idNode.Parent.Id, Is.EqualTo(parentNodeId));
        }

        [Test]
        public void CanCreateIdNodeWithNoParentAndChild()
        {
            const int nodeId = 2;
            const int childId = 3;

            var idNode = new IdNode(nodeId);
            idNode.Children.Add(new IdNode(childId, idNode));

            Assert.IsNotNull(idNode);
            Assert.That(idNode.Id, Is.EqualTo(nodeId));

        }

        [Test]
        public void ToStringReturnsIdParentIdAndChildrenCount()
        {
            const int nodeId = 2;
            const int childId = 3;
            const int parentId = 1;

            var idNode = new IdNode(nodeId, new IdNode(parentId));
            var childNode = new IdNode(childId, idNode);

            string toString = idNode.ToString();

            Assert.That(toString, Is.EqualTo("Id: 2, ParentId: 1, Children Count: 1"));
        }

    }
}
