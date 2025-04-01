const vulnerabilities = require('./vulnerabilities.json');
console.log('----------------------------------Checking Vulnerabilities----------------------------------');

const packagesList = vulnerabilities.vulnerabilities;
const packagesArray = Object.entries(packagesList);
const filteredPackages = packagesArray.filter(x => (x[1].severity?.toLowerCase() === 'high' ||  x[1].severity?.toLowerCase() === 'critical'));

if(filteredPackages.length > 0){
    console.log('Found ' + filteredPackages.length + ' High / Critical Vulnerabilities');
    const packageNames = filteredPackages.map(x => x[0]);
    console.log('Package Names:', packageNames);

    process.exit(1);
}

console.log('No Vulnerabilities Found :)');
process.exit(0);