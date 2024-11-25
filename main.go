package main

import (
	"archive/zip"
	"fmt"
	"io"
	"os"
	"path/filepath"
)

func main() {
	url := fmt.Sprintf("https://codeload.github.com/FortniteModdingHub/FNGameProj/zip/refs/heads/%%2B%%2BFortnite%%2BRelease-%s", getDesiredVersion())

	fmt.Print("Enter Project name\n> ")
	var projectName string
	for {
		fmt.Scanf("%s", &projectName)
		if projectName != "" {
			break
		}
	}

	downloadFile("./UEProject.zip", url)

	//TODO: Unzip UEProject.zip
	r, err := zip.OpenReader("./UEProject.zip")
	if err != nil {
		panic("Failed to construct reader!")
	}
	defer r.Close()

	for _, f := range r.File {
		rc, err := f.Open()
		if err != nil {
			panic("Cannot open UEProject zip file!")
		}
		defer rc.Close()

		pathname := filepath.Join(fmt.Sprintf("./%s", projectName), f.Name)
		if f.FileInfo().IsDir() {
			os.MkdirAll(pathname, f.Mode())
		} else {
			f, err := os.OpenFile(pathname, os.O_WRONLY|os.O_CREATE|os.O_TRUNC, f.Mode())
			if err != nil {
				panic("Couldn't open file in filesystem!")
			}
			defer f.Close()

			_, err = io.Copy(f, rc)
			if err != nil {
				panic("Could not write to new file!")
			}
		}
	}

	os.Remove("./UEProject.zip")

	fmt.Println(fmt.Sprintf("Successfully set up %s", projectName))
}
