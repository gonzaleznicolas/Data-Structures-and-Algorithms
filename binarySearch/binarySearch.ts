let sortedArray = [1,2,3,4,5];
let valueToFind = 0;
let index = binarySearch(sortedArray, valueToFind);
console.log(`In the array ${sortedArray} the index of the value ${valueToFind} is ${index}`);

// precondition: sortedAry is sorted with no repeated values
// returns: index of value, or -1 if value not in sortedAry
function binarySearch(sortedAry: number[], value: number) {
    return bsHelper(sortedAry, value, 0, sortedAry.length - 1)
}

// startIndex and endIndex are inclusive
// ary should be sorted
function bsHelper(ary: number[], value: number, startIndex: number, endIndex: number) {
    if (endIndex < startIndex) { // subarray is empty
        return -1;
    }

    let midIndex = Math.floor((startIndex + endIndex) / 2);

    if (ary[midIndex] === value) {
        return midIndex;
    } else if (value < ary[midIndex]) {
        return bsHelper(ary, value, startIndex, midIndex - 1);
    } else { // ary[midIndex] < value
        return bsHelper(ary, value, midIndex + 1, endIndex)
    }
}