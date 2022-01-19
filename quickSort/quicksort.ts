let array = [4, 9, 5, 0, 2, 90, 89, 2, 45];
console.log(partition(array, 0, array.length - 1));
console.log(array.join(','));

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