using System;
using WBSTree;
using NUnit.Framework;

namespace IdTreeSerializerTests
{
    [TestFixture]
    public class IdTreeSerializerTests
    {
        [Test]
        public void CanSerializeEmptyTree()
        {
            var idTree = new IdTree();
            var serializer = new IdTreeSerializer();

            byte[] serializedIdTree = serializer.Serialize(idTree);

            int deserializedId = BitConverter.ToInt32(serializedIdTree, 0);

            Assert.That(deserializedId, Is.EqualTo(IdNode.EmptyNodeId));
            
        }

        [Test]
        public void CanSerializeTreeWithSingleNodeWithNoParentAndNoChildren()
        {
            const int rootNodeId = 100;
            var rootNode = new IdNode(rootNodeId);
            var idTree = new IdTree(rootNode);

            var serializer = new IdTreeSerializer();
            byte[] serializedIdTree = serializer.Serialize(idTree);

            int actualRootNodeId = BitConverter.ToInt32(serializedIdTree, 0);
            int actualParentNodeId = BitConverter.ToInt32(serializedIdTree, 4);

            Assert.That(actualRootNodeId, Is.EqualTo(rootNodeId));
            Assert.That(actualParentNodeId, Is.EqualTo(IdNode.EmptyNodeId));

        }
        [Test]
        public void CanSerializeTreeWithOneChild()
        {
            const int rootNodeId = 100;
            const int childNodeId = 200;
            var rootNode = new IdNode(rootNodeId);
            var childNode = new IdNode(childNodeId, rootNode);
            var idTree = new IdTree(rootNode);

            var serializer = new IdTreeSerializer();
            byte[] serializedIdTree = serializer.Serialize(idTree);

            int actualRootNodeId = BitConverter.ToInt32(serializedIdTree, 0);
            int actualParentNodeId = BitConverter.ToInt32(serializedIdTree, 4);
            int actualChildCount = BitConverter.ToInt32(serializedIdTree, 8);
            int actualChildId = BitConverter.ToInt32(serializedIdTree, 12);

            Assert.That(actualRootNodeId, Is.EqualTo(rootNodeId));
            Assert.That(actualParentNodeId, Is.EqualTo(IdNode.EmptyNodeId));
            Assert.That(actualChildCount, Is.EqualTo(1));
            Assert.That(actualChildId, Is.EqualTo(childNodeId));
        }

        [Test]
        public void CanSerializeWbsTree()
        {
            var bomberTree = BuildBomberTree();

            var serializer = new IdTreeSerializer();
            byte[] serializedBomber = serializer.Serialize(bomberTree);

            Assert.That(serializedBomber.Length, Is.EqualTo(84));
        }

        [Test]
        public void CanDeserializeWbsTree()
        {
            IdTree bomberTree = BuildBomberTree();
            var serializer = new IdTreeSerializer();
            byte[] serializedBomber = serializer.Serialize(bomberTree);

            IdTree deserializedBomberTree = serializer.DeSerialize(serializedBomber);

            Assert.IsNotNull(deserializedBomberTree.RootNode);
            IdNode bomber = deserializedBomberTree.RootNode;
            Assert.That(bomber.Id, Is.EqualTo(100));

            IdNode fuselage = bomber.Children[0];
            Assert.That(fuselage.Id, Is.EqualTo(200));
            Assert.That(fuselage.Parent.Id, Is.EqualTo(100));

            IdNode engineerFuselage = fuselage.Children[0];
            Assert.That(engineerFuselage.Id, Is.EqualTo(300));

            IdNode testFuselage = fuselage.Children[1];
            Assert.That(testFuselage.Id, Is.EqualTo(400));

            IdNode bombs = bomber.Children[1];
            Assert.That(bombs.Id, Is.EqualTo(500));

            IdNode engineerBombs = bombs.Children[0];
            Assert.That(engineerBombs.Id, Is.EqualTo(600));

            IdNode testBombs = bombs.Children[1];
            Assert.That(testBombs.Id, Is.EqualTo(700));
        }

        private IdTree BuildBomberTree()
        {
            var bomber = new IdNode(100);
            var fuselage = new IdNode(200, bomber);
            var engineerFuselage = new IdNode(300, fuselage);
            var testFuselage = new IdNode(400, fuselage);
            var bombs = new IdNode(500, bomber);
            var engineerBombs = new IdNode(600, bombs);
            var testBombs = new IdNode(700, bombs);

            var wbsTree = new IdTree(bomber);
            return wbsTree;
        }
    }
}
