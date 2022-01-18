let sortedArray = [1,2,3,4,5];
let valueToFind = 6;
let index = binarySearch(sortedArray, valueToFind);
console.log(`In the array ${sortedArray} the index of the value ${valueToFind} is ${index}`);

// precondition: ary is sorted with no repeated values
// returns: index of value, or -1 if value not in sortedAry
function binarySearch(ary: number[], value: number) {
    let startIndex = 0; // inclusive
    let endIndex = ary.length; // exclusive
    let valueIndex = -1;
    while (startIndex < endIndex) {
        let midIndex = Math.floor((startIndex + endIndex) / 2);

        if (ary[midIndex] === value) {
            valueIndex = midIndex;
            break;
        } else if (ary[midIndex] < value) {
            startIndex = midIndex + 1;
        } else { // value < ary[midIndex]
            endIndex = midIndex;
        }
    }

    return valueIndex
}
