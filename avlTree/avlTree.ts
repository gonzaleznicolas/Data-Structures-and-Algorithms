class BstNode {
    key: number;
    value: number
    left: BstNode;
    right: BstNode;
    parent: BstNode;

    constructor(k: number, v: number, p: BstNode = undefined) {
        this.key = k;
        this.value = v;
        this.left = undefined;
        this.right = undefined;
        this.parent = p;
    }

    get hasLeft() {
        return this.left !== undefined;
    }

    get hasRight() {
        return this.right !== undefined;
    }

    get isLeaf() {
        return !this.hasLeft && !this.hasRight;
    }

    get hasOnlyOneChild() {
        return (this.hasLeft && !this.hasRight) || (!this.hasLeft && this.hasRight);
    }

    get onlyChild(): BstNode {
        if (this.hasOnlyOneChild) {
            return this.hasLeft ? this.left : this.right;
        } else {
            throw Error("Cannot call onlyChild on node with two children");
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
}

class AvlTree {
    root: BstNode;

    constructor() {
        this.root = undefined;
    }

    get isEmpty() {
        return this.root === undefined;
    }

    insert(newKey: number, newVal: number): void {
        if (this.isEmpty) {
            this.root = new BstNode(newKey, newVal, undefined);
        } else {
            let pointer = this.root;
            while(true) {
                if (newKey === pointer.key) {
                    throw Error("Key already exists");
                } else if (newKey < pointer.key) {
                    if (pointer.left === undefined) {
                        pointer.left = new BstNode(newKey, newVal, pointer);
                        break;
                    } else {
                        pointer = pointer.left;
                    }
                } else { // newNode.key > pointer.key
                    if (pointer.right === undefined) {
                        pointer.right = new BstNode(newKey, newVal, pointer);
                        break;
                    } else {
                        pointer = pointer.right;
                    }
                }
            }
        }
    }

    search(searchKey: number): BstNode {
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
    findMin(node: BstNode): BstNode {
        let pointer = node;
        while (pointer.hasLeft) {
            pointer = pointer.left;
        }
        return pointer;
    }

    delete(deleteKey: number) {
        let d = this.search(deleteKey);
        if (d.isLeaf) {
            d.parent.removeChildWithKey(deleteKey);
        } else if (d.hasOnlyOneChild) {
            d.onlyChild.parent = d.parent;
            if (d.parent.left.key === d.key) {
                d.parent.left = d.onlyChild;
            } else {
                d.parent.right = d.onlyChild;
            }
        } else { // has two children
            let minInRightSubTree = this.findMin(d.right);
            minInRightSubTree.parent.removeChildWithKey(minInRightSubTree.key)
            if (d.parent.left.key === d.key) {
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
    }
}

let bst = new AvlTree();
bst.insert(50,50);
bst.insert(30,30);
bst.insert(70,70);
bst.insert(10,10);
bst.insert(40,40);
bst.insert(60,60);
bst.insert(80,80);
bst.insert(33,33);
bst.insert(45,45);
bst.delete(80);
bst.delete(70);
bst.delete(30);
console.log("hi");