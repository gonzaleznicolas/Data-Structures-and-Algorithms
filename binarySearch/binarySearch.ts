let sortedArray = [1,2,3,4,5];
let valueToFind = 5;
let index = binarySearch(sortedArray, valueToFind);
console.log(`In the array ${sortedArray} the index of the value ${valueToFind} is ${index}`);

// returns index of first instance of value, or -1 if value not in sortedAry
function binarySearch(sortedAry: number[], value: number) {
    return bsHelper(sortedAry, value, 0, sortedAry.length - 1)
}

// startIndex and endIndex are inclusive
// ary should be sorted
function bsHelper(ary: number[], value: number, startIndex: number, endIndex: number) {
    if (endIndex < startIndex) { // subarray is empty
        return -1;
    }

    if (startIndex === endIndex) {
        return ary[startIndex] === value ? startIndex : -1;
    }

    let midIndex = Math.floor((startIndex + endIndex) / 2);
    let leftResult = bsHelper(ary, value, startIndex, midIndex);
    if (leftResult !== -1) {
        return leftResult;
    }

    return bsHelper(ary, value, midIndex + 1, endIndex);
}