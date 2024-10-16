import {format} from "date-fns";

export class DateTimeUtils {
	static toUserLocalDate(utcDate: string): string {
		let date = new Date(this.toUTCDateString(utcDate))

		return format(date.toLocaleDateString(), "dd/MM/yyyy")
	}

	private static toUTCDateString(utcDate: string): string {
		return `${utcDate}Z`
	}
}
