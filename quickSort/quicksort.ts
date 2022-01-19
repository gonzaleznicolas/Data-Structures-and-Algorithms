let array = [4, 9, 5, 0, 2, 90, 89, 2, 45];
quickSort(array);
console.log(array.join(','));

function quickSort(ary: number[]) {
    quickSortHelper(ary, 0, ary.length - 1);
}

function quickSortHelper(ary: number[], low: number, high: number) {
    if (low >= high) {
        return;
    }
    
    let q = partition(ary, low, high); // index of element placed in its final position
    quickSortHelper(ary, low, q - 1);
    quickSortHelper(ary, q + 1, high);
}

function partition(ary: number[], low: number, high: number): number {
    let p = ary[high]; // pivot
    let i = low;
    let j = high - 1;

    while (i < j) {
        while (ary[i] <= p && i <= j) {
            i++;
        }

        while (ary[j] >= p && j >= i) {
            j--;
        }

        if (i < j) {
            // swap ary[i] and ary[j]
            let temp = ary[i];
            ary[i] = ary[j];
            ary[j] = temp;
        }
    }

    // swap p and ary[i]
    ary[high] = ary[i];
    ary[i] = p;

    return i;
}