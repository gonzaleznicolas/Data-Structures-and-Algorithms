class LlqNode {
    n: number;
    next: LlqNode;
}

class LinkedListQueue {
    head: LlqNode;
    tail: LlqNode;
    count: number;

    constructor() {
        this.head = undefined;
        this.tail = undefined;
    }

    get isEmpty() {
        return this.head === undefined && this.tail === undefined;
    }

    print() {
        let pointer = this.head;
        while (pointer !== undefined) {
            console.log(`${pointer.n}${pointer === this.head ? " <== head" : ""}`);
            pointer = pointer.next;
        }
    }

    add(n: number) {
        console.log(`\nadd(${n})`);
        let newNode = <LlqNode> {n: n, next: undefined};
        if (this.isEmpty) {
            this.head = newNode;
            this.tail = newNode;
        } else {
            this.tail.next = newNode;
            this.tail = newNode;
        }
        this.print();
    }

    remove(): number | void {
        console.log(`\nremove()`);
        if (!this.isEmpty) {
            let h = this.head;
            this.head = this.head.next;
            if (this.head === undefined) {
                // queue is empty
                this.tail = undefined;
            }
            this.print();
            console.log(`return ${h.n}`);
            return h.n;
        } else {
            console.log("The queue is empty. Cannot remove.");
            this.print();
        }
    }
}

let llq = new LinkedListQueue();
llq.add(1);
llq.add(2);
llq.add(3);
llq.add(4);
llq.add(5);
llq.add(6);
llq.add(7);
llq.add(8);
llq.add(9);
llq.add(10);
llq.remove();
llq.remove();
llq.remove();
llq.remove();
llq.remove();
llq.remove();
llq.remove();
llq.remove();
llq.remove();
llq.remove();
llq.remove();
llq.remove();
