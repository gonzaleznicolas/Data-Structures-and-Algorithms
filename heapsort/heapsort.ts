class HeapSort {
    ary: number[];
    size: number;

    constructor(arrayToSort: number[]) {
        this.ary = arrayToSort;
        this.size = 0;
        this.sort();
    }

    sort() {
        for (let i = 0; i < this.ary.length; i++) {
            this.insert(this.ary[i]);
        }

        for (let i = this.ary.length - 1; i >= 0; i--) {
            this.ary[i] = this.deleteMax();
        }
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
        while (i < this.size) {
            let leftChildVal = this.left(i) < this.size ? this.ary[this.left(i)] : undefined;
            let rightChildVal = this.right(i) < this.size ? this.ary[this.right(i)] : undefined;

            if (leftChildVal === undefined && rightChildVal === undefined) { // no children
                break;
            }
            else if (leftChildVal !== undefined && rightChildVal === undefined) { // one child
                if (leftChildVal > numToMoveDown) {
                    // swap with left
                    this.ary[i] = this.ary[this.left(i)];
                    this.ary[this.left(i)] = numToMoveDown;
                    i = this.left(i);
                } else {
                    break;
                }
            } else if (leftChildVal !== undefined && rightChildVal !== undefined) { // two children
                if (leftChildVal > rightChildVal) {
                    // swap with left
                    this.ary[i] = leftChildVal;
                    this.ary[this.left(i)] = numToMoveDown;
                    i = this.left(i);
                } else if (rightChildVal > leftChildVal) {
                    // swap with right
                    this.ary[i] = rightChildVal;
                    this.ary[this.right(i)] = numToMoveDown;
                    i = this.right(i);
                } else {
                    break;
                }
            } else {
                break;
            }
        }

        console.log(`deleteMax() returns ${num}`);
        this.print();
        return num;
    }
}

let a = [9, 8, 7, 6, 5, 4, 3, 1];
new HeapSort(a);
console.log(a.join(", "));
