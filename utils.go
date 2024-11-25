package main

import (
	"fmt"
	"io"
	"net/http"
	"os"
)

func downloadFile(filepath string, url string) (err error) {
	out, err := os.Create(filepath)
	if err != nil {
		return err
	}
	defer out.Close()

	resp, err := http.Get(url)
	if err != nil {
		return err
	}
	defer resp.Body.Close()

	_, err = io.Copy(out, resp.Body)
	if err != nil {
		return err
	}

	return nil
}

func isValidVersion(version float32) bool {
	switch version {
	case 7.4, 8.51, 9.1, 14.3:
		return true
	}
	return false
}

func getDesiredVersion() string {
	fmt.Print("Enter desired project version (7.40, 8.51, 9.10, 14.30)\n> ")

	var version float32
	for {
		fmt.Scanf("%f", &version)
		valid := isValidVersion(version)
		if valid {
			break
		}
	}

	return fmt.Sprintf("%.2f", version)
}
