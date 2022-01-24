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
}

class BinarySearchTree {
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
        while (pointer.left !== undefined) {
            pointer = pointer.left;
        }
        return pointer;
    }
}

let bst = new BinarySearchTree();
bst.insert(5,5);
bst.insert(4,4);
bst.insert(2,2);
bst.insert(6,6);
bst.insert(7,7);
bst.insert(0,0);
console.log(bst.search(5).value);
console.log(bst.search(4).value);
console.log(bst.search(2).value);
console.log(bst.search(6).value);
console.log(bst.search(7).value);
console.log(bst.search(0).value);
console.log(bst.findMin(bst.root).value);