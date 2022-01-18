let aryToSort = [5, 6, 7, 2, 7, 4, 8];

let ary1 = [];
let ary2 = [1];

console.log(merge(ary1, ary2).join(','));

// precondition: a and b are sorted
function merge(a: number[], b: number[]): number[] {
    let mergedAry = Array(a.length + b.length);
    let ia = 0;
    let ib = 0;
    let im = 0;
    while (ia <= a.length && ib <= b.length) {
        if ((ia < a.length && a[ia] <= b[ib]) || b.length <= ib) {
            mergedAry[im] = a[ia];
            ia++;
        } else {
            mergedAry[im] = b[ib];
            ib++;
        }
        im++;
    }

    return mergedAry;
}