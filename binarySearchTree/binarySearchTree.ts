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
}

let bst = new BinarySearchTree();
bst.insert(5,5);
bst.insert(4,4);
bst.insert(2,2);
bst.insert(6,6);
bst.insert(7,7);
bst.insert(0,0);
