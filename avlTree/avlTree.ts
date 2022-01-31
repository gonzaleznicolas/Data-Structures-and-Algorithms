class AvlNode {
    key: number;
    value: number
    height: number;
    left: AvlNode;
    right: AvlNode;
    parent: AvlNode;

    constructor(k: number, v: number, p: AvlNode = undefined, h: number) {
        this.key = k;
        this.value = v;
        this.left = undefined;
        this.right = undefined;
        this.parent = p;
        this.height = h;
    }

    // returns the root of the tree after the rotation
    rightLeftDoubleRotate(): AvlNode {
        this.right.rightRotate();
        return this.leftRotate();
    }

    // returns the root of the tree after the rotation
    leftRightDoubleRotate(): AvlNode {
        this.left.leftRotate();
        return this.rightRotate();
    }

    // returns the root of the tree after the rotation
    rightRotate(): AvlNode {
        this.left.parent = this.parent;
        if (this.hasParent && this.parent.hasLeft && this.parent.left.key === this.key) {
            this.parent.left = this.left;
        } else if (this.hasParent) {
            this.parent.right = this.left;
        }
        this.parent = this.left;
        this.left = this.parent.right;
        if (this.parent.hasRight) {
            this.parent.right.parent = this;
        }
        this.parent.right = this;
        this.updateHeightBasedOnChildren();
        this.parent.updateHeightBasedOnChildren();
        return this.parent;
    }

    // returns the root of the tree after the rotation
    leftRotate(): AvlNode {
        this.right.parent = this.parent;
        if (this.hasParent && this.parent.hasLeft && this.parent.left.key === this.key) {
            this.parent.left = this.right;
        } else if (this.hasParent) {
            this.parent.right = this.right;
        }
        this.parent = this.right;
        this.right = this.parent.left;
        if (this.parent.hasLeft) {
            this.parent.left.parent = this;
        }
        this.parent.left = this;
        this.updateHeightBasedOnChildren();
        this.parent.updateHeightBasedOnChildren();
        return this.parent;
    }

    updateHeightBasedOnChildren() {
        if (this.hasOnlyOneChild) {
            this.height = this.onlyChild.height + 1;
        } else if (this.isLeaf) {
            this.height = 0;
        } else { // has two children
            this.height = Math.max(this.left.height, this.right.height) + 1;
        }
    }

    removeChildWithKey(k: number) {
        if (this.hasRight && this.right.key === k) {
            this.right.parent = undefined;
            this.right = undefined;
        } else if (this.hasLeft && this.left.key === k) {
            this.left.parent = undefined;
            this.left = undefined;
        } else {
            throw Error("Neither child has that key.");
        }
    }

    get hasParent() {
        return this.parent !== undefined;
    }

    get balanceFactor() {
        return this.leftHeight - this.rightHeight;
    }

    get hasLeft() {
        return this.left !== undefined;
    }

    get leftHeight() {
        if (this.hasLeft) {
            return this.left.height;
        } else {
            return -1;
        }
    }

    get hasRight() {
        return this.right !== undefined;
    }

    get rightHeight() {
        if (this.hasRight) {
            return this.right.height;
        } else {
            return -1;
        }
    }

    get isLeaf() {
        return !this.hasLeft && !this.hasRight;
    }

    get hasOnlyOneChild() {
        return (this.hasLeft && !this.hasRight) || (!this.hasLeft && this.hasRight);
    }

    get onlyChild(): AvlNode {
        if (this.hasOnlyOneChild) {
            return this.hasLeft ? this.left : this.right;
        } else {
            throw Error("Cannot call onlyChild on node with two children");
        }
    }
}

class AvlTree {
    root: AvlNode;

    constructor() {
        this.root = undefined;
    }

    get isEmpty() {
        return this.root === undefined;
    }

    insert(newKey: number, newVal: number): void {
        if (this.isEmpty) {
            this.root = new AvlNode(newKey, newVal, undefined, 0);
        } else {
            let pointer = this.root;
            while (true) {
                if (newKey === pointer.key) {
                    throw Error("Key already exists");
                } else if (newKey < pointer.key) {
                    if (pointer.left === undefined) {
                        pointer.left = new AvlNode(newKey, newVal, pointer, 0);
                        pointer = pointer.left;
                        break;
                    } else {
                        pointer = pointer.left;
                    }
                } else { // newNode.key > pointer.key
                    if (pointer.right === undefined) {
                        pointer.right = new AvlNode(newKey, newVal, pointer, 0);
                        pointer = pointer.right;
                        break;
                    } else {
                        pointer = pointer.right;
                    }
                }
            }

            while (pointer !== undefined) {
                pointer.updateHeightBasedOnChildren();
                if (pointer.balanceFactor > 1) { // node was inserted in left subtree
                    if (pointer.left.leftHeight >= pointer.left.rightHeight) { // left outer case
                        let r = pointer.rightRotate();
                        if (!r.hasParent) {
                            this.root = r;
                        }
                        break; // for insertion, after a rotation is performed, there is no need to continue walking up the tree
                    } else { // left inner case
                        let r = pointer.leftRightDoubleRotate();
                        if (!r.hasParent) {
                            this.root = r;
                        }
                        break;
                    }
                } else if (pointer.balanceFactor < -1) { // node was inserted in the right subtree
                    if (pointer.right.rightHeight >= pointer.right.leftHeight) { // right outer case
                        let r = pointer.leftRotate();
                        if (!r.hasParent) {
                            this.root = r;
                        }
                        break;
                    } else { // right inner case
                        let r = pointer.rightLeftDoubleRotate();
                        if (!r.hasParent) {
                            this.root = r;
                        }
                        break;
                    }
                }
                pointer = pointer.parent;
            }
        }
    }

    search(searchKey: number): AvlNode {
        let pointer = this.root;
        while(pointer !== undefined) {
            if (searchKey === pointer.key) {
                break;
            } else if (searchKey < pointer.key) {
                pointer = pointer.left;
            } else { // searchKey > pointer.key
                pointer = pointer.right;
            }
        }
        if (pointer === undefined) {
            throw Error("Not found.");
        }
        return pointer;
    }

    // returns the min BstNode in the tree rooted at node
    findMin(node: AvlNode): AvlNode {
        let pointer = node;
        while (pointer.hasLeft) {
            pointer = pointer.left;
        }
        return pointer;
    }

    delete(deleteKey: number) {
        let lowestNodeOnPathFromDeletedNodeToRoot: AvlNode;

        let d = this.search(deleteKey);
        if (d.isLeaf) {
            lowestNodeOnPathFromDeletedNodeToRoot = d.parent;
            d.parent.removeChildWithKey(deleteKey);
        } else if (d.hasOnlyOneChild) {
            lowestNodeOnPathFromDeletedNodeToRoot = d.onlyChild;
            d.onlyChild.parent = d.parent;
            if (d.parent.hasLeft && d.parent.left.key === d.key) {
                d.parent.left = d.onlyChild;
            } else {
                d.parent.right = d.onlyChild;
            }
        } else { // has two children
            let minInRightSubTree = this.findMin(d.right);
            lowestNodeOnPathFromDeletedNodeToRoot = minInRightSubTree.parent;
            minInRightSubTree.parent.removeChildWithKey(minInRightSubTree.key)
            if (d.parent.hasLeft && d.parent.left.key === d.key) {
                d.parent.left = minInRightSubTree;
            } else { // d.parent.right.key === d.key
                d.parent.right = minInRightSubTree;
            }
            minInRightSubTree.parent = d.parent;
            d.parent = undefined;

            d.left.parent = minInRightSubTree;
            minInRightSubTree.left = d.left;
            d.left = undefined;

            d.right.parent = minInRightSubTree;
            minInRightSubTree.right = d.right;
            d.right = undefined;
        }

        let pointer = lowestNodeOnPathFromDeletedNodeToRoot;
        while (pointer !== undefined) {
            pointer.updateHeightBasedOnChildren();
            pointer = pointer.parent;
        }
    }
}

let avlt = new AvlTree();
avlt.insert(50,50);
avlt.insert(30,30);
avlt.insert(90,90);
avlt.insert(70,70);
avlt.insert(100,100);
avlt.insert(75,75);
console.log("hi");
