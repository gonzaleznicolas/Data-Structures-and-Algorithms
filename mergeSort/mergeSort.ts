let aryToSort = [8, 7, 6, 5, 4, 3, 2];
mergeSort(aryToSort);
console.log(aryToSort.join(","));

function mergeSort(ary: number[]) {
    let space = new Array<number>(ary.length);
    mergeSortHelper(ary, space, 0, ary.length);
}

// start is inclusive, end is exclusive
function mergeSortHelper(ary: number[], space: number[], start: number, end: number) {
    if (end <= start + 1) {
        return;
    }

    let mid = Math.floor((start + end) / 2);

    mergeSortHelper(ary, space, start, mid); // sort left
    mergeSortHelper(ary, space, mid, end); // sort right
    merge(ary, space, start, mid, end)
}

// precondition: start - mid(exclusive), and mid - end(exclusive) are sorted
// postcondition: merged sorted arrays in start - end(exclusive)
function merge(ary: number[], space: number[], start: number, mid: number, end: number) {
    // copy start-end into the space
    for (let i = start; i < end; i++) {
        space[i] = ary[i];
    }

    let il = start; // index left
    let ir = mid; // index right
    let im = start; // index merged
    while (il < mid && ir < end) {
        if (space[il] <= space[ir]) {
            ary[im] = space[il];
            il++;
        } else {
            ary[im] = space[ir];
            ir++;
        }
        im++;
    }

    while (il < mid) {
        ary[im] = space[il];
        il++;
        im++;
    }

    while (ir < end) {
        ary[im] = space[ir];
        ir++;
        im++;
    }
}