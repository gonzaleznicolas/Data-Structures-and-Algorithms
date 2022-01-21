class ArrayUnboundedQueue {
    ary: number[];
    head: number; // next element to be removed
    tail: number; // place to put next added element
    count: number; // count

    constructor() {
        this.ary = new Array<number>(4);
        this.head = 0;
        this.tail = 0;
        this.count = 0;
    }

    get isEmpty() {
        return this.count === 0;
    }

    get capacity() {
        return this.ary.length;
    }

    get isFull() {
        return this.ary.length === this.count;
    }

    nextIndex(i: number) {
        return (++i) % this.ary.length;
    }

    indexPlusOffset(i: number, offset: number) {
        return (i + offset) % this.ary.length;
    }

    print() {
        for (let offset = 0; offset < this.count; offset++) {
            let i = this.indexPlusOffset(this.head, offset);
            console.log(`${this.ary[i]}${ i === this.head ? " <== head" : ""}`);
            i = this.nextIndex(i);
        }
        console.log();
    }

    add(n: number) {
        console.log(`\nadd(${n})`);
        if (!this.isFull) {
            this.count++;
            this.ary[this.tail] = n;
            this.tail = this.nextIndex(this.tail);
        }
        this.print();
    }

    remove(): number {
        console.log(`\nremove()`);
        let n: number;
        if (!this.isEmpty) {
            this.count--;
            n = this.ary[this.head];
            this.head = this.nextIndex(this.head);
        }
        this.print();
        console.log(`return ${n}`);
        return n;
    }
}

let q = new ArrayUnboundedQueue();
q.add(1);
q.add(2);
q.add(3);
q.add(4);
q.add(5);
q.remove();
q.remove();
q.remove();
q.remove();
q.remove();
q.remove();