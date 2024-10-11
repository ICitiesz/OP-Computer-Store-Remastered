import {format} from "date-fns";

export class DateTimeUtils {
	static toUserLocalDate = (utcDate: string) => {
		let date = new Date(this.toUTCDateString(utcDate))

		return format(date.toLocaleDateString(), "dd/MM/yyyy")
	}

	private static toUTCDateString = (utcDate: string) => {
		return `${utcDate}Z`
	}
}
