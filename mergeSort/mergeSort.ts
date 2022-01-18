let aryToSort = [5, 6, 7, 2, 7, 4, 8];
console.log(mergeSort(aryToSort).join(","));

function mergeSort(ary: number[]): number[] {
    if (ary.length === 1) {
        return ary;
    }

    let firstIndexOfRight = Math.floor(ary.length / 2);
    let left = ary.slice(0, firstIndexOfRight);
    let right = ary.slice(firstIndexOfRight, ary.length + 1);
    
    return merge(mergeSort(left), mergeSort(right));
}

// precondition: a and b are sorted
function merge(a: number[], b: number[]): number[] {
    let mergedAry = new Array(a.length + b.length);
    let ia = 0;
    let ib = 0;
    let im = 0;

    while (ia < a.length && ib < b.length) {
        if (a[ia] <= b[ib]) {
            mergedAry[im] = a[ia];
            ia++;
        } else {
            mergedAry[im] = b[ib];
            ib++;
        }
        im++;
    }

    while (ia < a.length) {
        mergedAry[im] = a[ia];
        ia++;
        im++;
    }

    while (ib < b.length) {
        mergedAry[im] = b[ib];
        ib++;
        im++;
    }

    return mergedAry;
}