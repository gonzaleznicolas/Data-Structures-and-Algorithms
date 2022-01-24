class BstNode {
    key: number;
    value: number
    left: BstNode;
    right: BstNode;

    constructor(k: number, v: number) {
        this.key = k;
        this.value = v;
        this.left = undefined;
        this.right = undefined;
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

    insert(k: number, v: number) {
        let newNode = new BstNode(k, v);
        if (this.isEmpty) {
            this.root = newNode;
        } else {
            let pointer = this.root;
            while(true) {
                if (newNode.key === pointer.key) {
                    throw Error("Key already exists");
                } else if (newNode.key < pointer.key) {
                    if (pointer.left === undefined) {
                        pointer.left = newNode;
                        break;
                    } else {
                        pointer = pointer.left;
                    }
                } else { // newNode.key > pointer.key
                    if (pointer.right === undefined) {
                        pointer.right = newNode;
                        break;
                    } else {
                        pointer = pointer.right;
                    }
                }
            }
        }
    }

    search(searchKey: number) {
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
        return pointer.value;
    }

    // returns the min BstNode in the tree rooted at node
    findMin(node: BstNode) {
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
console.log(bst.search(5));
console.log(bst.search(4));
console.log(bst.search(2));
console.log(bst.search(6));
console.log(bst.search(7));
console.log(bst.search(0));
bst.findMin(bst.root);