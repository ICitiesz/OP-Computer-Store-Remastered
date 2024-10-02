import {DomSanitizer, SafeResourceUrl, SafeUrl, SafeValue} from "@angular/platform-browser";
import {inject} from "@angular/core";
import {IconHolder} from "../icon-holder";
import {MatIconRegistry} from "@angular/material/icon";

export const getSafeResourceUrl = (rawResourceUrl: string) => {
	const domSanitizer = inject(DomSanitizer)

	return domSanitizer.bypassSecurityTrustResourceUrl(rawResourceUrl);
}

export const registerIcon = (iconHolder: IconHolder) => {
	const matIconRegistry = inject(MatIconRegistry)

	matIconRegistry.addSvgIcon(iconHolder.iconName, getSafeResourceUrl(iconHolder.iconUrl))
}

export const func2 = () => {

}
