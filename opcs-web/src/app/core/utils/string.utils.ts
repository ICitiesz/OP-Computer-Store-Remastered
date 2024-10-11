export class StringUtils {
	static startWith = (str1: string, str2: string, ignoreCase: boolean = true): boolean => {
		let regex

		if (ignoreCase) {
			regex = new RegExp(`^${str2}`, 'i');
		} else {
			regex = new RegExp(`^${str2}`);
		}

		return regex.test(str1)
	}

	static containsString = (str1: string, str2: string, ignoreCase: boolean = true): boolean => {
		let regex

		if (ignoreCase) {
			regex = new RegExp(`.*${str2}.*`, 'i')
		} else {
			regex = new RegExp(`.*${str2}`);
		}

		return regex.test(str1)
	}
}


