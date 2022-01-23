class MaxHeap {
    ary: number[];
    size: number;

    constructor(capacity: number) {
        this.ary = new Array<number>(capacity);
        this.size = 0;
    }

    get isFull() {
        return this.size === this.ary.length;
    }

    get isEmpty() {
        return this.size === 0;
    }

    // left child index
    left(i: number): number {
        return 2 * i + 1;
    }

    // right child index
    right(i: number): number {
        return 2 * i + 2;
    }

    // parent index
    parent(i: number): number {
        return Math.floor((i - 1) / 2);
    }

    isRoot(i: number) {
        return i === 0;
    }

    print() {
        console.log("MaxHeap:");
        for (let i = 0; i < this.size; i++) {
            console.log(this.ary[i]);
        }
    }

    insert(num: number) {
        console.log(`insert(${num})`)
        if (this.isFull) {
            throw Error("MaxHeap is full");
        }

        // insert at last position
        this.ary[this.size] = num;
        this.size++;

        // swap up until smaller than its parent
        let i = this.size - 1; // index of last element
        while (this.ary[i] > this.ary[this.parent(i)]) {
            this.ary[i] = this.ary[this.parent(i)];
            this.ary[this.parent(i)] = num;
            i = this.parent(i);
        }

        this.print();
    }

    deleteMax(): number {
        if (this.isEmpty) {
            throw Error("MaxHeap is empty");
        }

        let num = this.ary[0];

        // put the last element in the root position
        this.ary[0] = this.ary[this.size - 1];
        this.size--;

        let i = 0;
        let numToMoveDown = this.ary[i];
        while (this.ary[i] < this.ary[this.left(i)] || this.ary[i] < this.ary[this.right[i]]) {
            if (this.ary[this.left(i)] >= this.ary[this.right(i)]) {
                // left child is >=. Swap with left
                this.ary[i] = this.ary[this.left(i)];
                this.ary[this.left(i)] = numToMoveDown;
                i = this.left(i);
            } else {
                // right child is >. Swap with right
                this.ary[i] = this.ary[this.right(i)];
                this.ary[this.right(i)] = numToMoveDown;
                i = this.right(i);
            }
        }

        console.log(`deleteMax() returns ${num}`);
        this.print();
        return num;
    }
}

let mH = new MaxHeap(7);
mH.insert(5);
mH.insert(4);
mH.insert(8);
mH.insert(1);
mH.insert(10);
mH.insert(0);
mH.insert(12);
mH.deleteMax();
mH.deleteMax();
mH.deleteMax();
mH.deleteMax();
mH.deleteMax();
mH.deleteMax();
mH.deleteMax();
mH.deleteMax();
