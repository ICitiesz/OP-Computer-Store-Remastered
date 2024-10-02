import {HttpResponse} from "@angular/common/http";

export interface ApiResponse<T> {
	contentType: string;
	statusCode: number;
	message: string;
	result: T;
}
